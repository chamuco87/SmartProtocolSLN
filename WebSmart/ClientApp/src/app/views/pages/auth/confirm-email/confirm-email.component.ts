// Angular
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
// RxJS
import { finalize, takeUntil, tap } from 'rxjs/operators';
import { Subject } from 'rxjs';
// Translate
import { TranslateService } from '@ngx-translate/core';
// Auth
import { AuthNoticeService, AuthService } from '../../../../core/auth';
import { ActivatedRoute } from '@angular/router';
@Component({
	selector: 'kt-confirm-email',
	templateUrl: './confirm-email.component.html',
	encapsulation: ViewEncapsulation.None
})
export class ConfirmEmailComponent implements OnInit, OnDestroy {
	// Public params
	forgotPasswordForm: FormGroup;
	loading = false;
	errors: any = [];

	private unsubscribe: Subject<any>; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/
	private sub: any;
	tokenId: string;
	/**
	 * Component constructor
	 *
	 * @param authService
	 * @param authNoticeService
	 * @param translate
	 * @param router
	 * @param fb
	 * @param cdr
	 */
	constructor(
		private authService: AuthService,
		public authNoticeService: AuthNoticeService,
		private translate: TranslateService,
		private router: Router,
		private cdr: ChangeDetectorRef,
		private route: ActivatedRoute
	) {
		this.unsubscribe = new Subject();
	}

	/**
	 * @ Lifecycle sequences => https://angular.io/guide/lifecycle-hooks
	 */

	/**
	 * On init
	 */
	ngOnInit() {
		this.sub = this.route.params.subscribe(params => {
			this.tokenId = params['tokenId']; // (+) converts string 'id' to a number
			this.authService.validateToken(this.tokenId).pipe(
				tap(response => {
					if (response.email) {
						this.authNoticeService.setNotice('Thanks for confirming your email ' + response.email, 'success');
						this.router.navigateByUrl('/auth/login');
					} else {
						this.authNoticeService.setNotice('We were not able to validate the token', 'danger');
						this.router.navigateByUrl('/auth/login');
					}
				}),
				takeUntil(this.unsubscribe),
				finalize(() => {
					this.loading = false;
					this.cdr.markForCheck();
				})
			).subscribe();
		});
	}

	/**
	 * On destroy
	 */
	ngOnDestroy(): void {
		this.unsubscribe.next();
		this.unsubscribe.complete();
		this.loading = false;
	}

}
