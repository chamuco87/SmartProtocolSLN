// Angular
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewEncapsulation, NgModule } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

// RxJS
import { finalize, takeUntil, tap } from 'rxjs/operators';
import { HttpClientModule } from '@angular/common/http';
// Translate
import { TranslateService } from '@ngx-translate/core';
// NGRX
import { Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
// Auth
import { AuthNoticeService, AuthService, Register, User, Login} from '../../../../core/auth/';
import { Subject } from 'rxjs';
import { ConfirmPasswordValidator } from './../register/confirm-password.validator';
import { EmailExistsValidator } from './../register/email-exists.validator';



@Component({
  selector: 'kt-reset-password',
  templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent implements OnInit, OnDestroy {
	registerForm: FormGroup;
	loading = false;
	errors: any = [];

	private unsubscribe: Subject<any>; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/

	/**
	 * Component constructor
	 *
	 * @param authNoticeService: AuthNoticeService
	 * @param translate: TranslateService
	 * @param router: Router
	 * @param auth: AuthService
	 * @param store: Store<AppState>
	 * @param fb: FormBuilder
	 * @param cdr
	 */
	constructor(
		private authNoticeService: AuthNoticeService,
		private translate: TranslateService,
		private router: Router,
		private auth: AuthService,
		private store: Store<AppState>,
		private fb: FormBuilder,
		private cdr: ChangeDetectorRef,
		private route: ActivatedRoute
	) {
		this.unsubscribe = new Subject();
	}

	// Public params
	forgotPasswordForm: FormGroup;
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
	


	ngOnInit() {
		this.initRegisterForm();
		this.sub = this.route.params.subscribe(params => {
			this.tokenId = params['tokenId']; // (+) converts string 'id' to a number
			this.auth.validateResetToken(this.tokenId).pipe(
				tap(response => {
					if (response._userId) {
						this.authNoticeService.setNotice('Thanks for verify your identity lets change your password', 'success');
						
						//this.router.navigateByUrl('/auth/login');
					} else {
						this.authNoticeService.setNotice('The link is expired, try again.', 'danger');
						this.router.navigateByUrl('/auth/forgot-password');
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

	ngOnDestroy(): void {
		this.unsubscribe.next();
		this.unsubscribe.complete();
		this.loading = false;
	}

	/**
	 * Form initalization
	 * Default params, validators
	 */
	initRegisterForm() {
		this.registerForm = this.fb.group({
			//fullname: ['', Validators.compose([
			//	Validators.required,
			//	Validators.minLength(3),
			//	Validators.maxLength(100)
			//])
			//],
			//email: new FormControl('', [
			//	Validators.required,
			//	Validators.email,
			//	Validators.minLength(3),
			//	// https://stackoverflow.com/questions/386294/what-is-the-maximum-length-of-a-valid-email-address
			//	Validators.maxLength(320),
			//	/**/],
			//	[EmailExistsValidator.EmailExists(this.auth)]

			//),
			//username: ['', Validators.compose([
			//	Validators.required,
			//	Validators.minLength(3),
			//	Validators.maxLength(100)
			//]),
			//],
			password: ['', Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])
			],
			confirmPassword: ['', Validators.compose([
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(100)
			])
			]
			//agree: [false, Validators.compose([Validators.required])]
		}, {
			//asyncValidators: [EmailExistsValidator.EmailExists(this.auth)],
			validator: [ConfirmPasswordValidator.MatchPassword]
		});
	}


	onEnter(event) {
		if (event.code == 'Enter') {
			event.preventDefault();
			this.submit();
		}
	}

	/**
	 * Form Submit
	 */
	submit() {
		const controls = this.registerForm.controls;

		// check form
		if (this.registerForm.invalid) {
			Object.keys(controls).forEach(controlName =>
				controls[controlName].markAsTouched()
			);
			return;
		}


		//if (!controls.agree.value) {
		//	// you must agree the terms and condition
		//	// checkbox cannot work inside mat-form-field https://github.com/angular/material2/issues/7891
		//	this.authNoticeService.setNotice('You must agree the terms and condition', 'danger');
		//	return;
		//}

		this.loading = true;

		const _user: User = new User();
		_user.clear();
		_user.email = this.tokenId;
		//_user.username = controls.username.value;
		//_user.fullname = controls.fullname.value;
		_user.password = controls.password.value;
		_user.roles = [];
		this.auth.resetPassword(_user).pipe(
			tap(user => {
				if (user) {
					if (user) {
						this.store.dispatch(new Login({ authToken: user.accessToken }));
						this.router.navigateByUrl('/dashboard'); // Main page
					}
					//this.store.dispatch(new Register({ authToken: user.accessToken }));
					//// pass notice message to the login page
					//this.authNoticeService.setNotice(this.translate.instant('AUTH.REGISTER.SUCCESS'), 'success');
					//this.router.navigateByUrl('/auth/login');
				} else {
					this.authNoticeService.setNotice(this.translate.instant('We have problems craeting your account, try again later'), 'danger');
				}
			}),
			takeUntil(this.unsubscribe),
			finalize(() => {
				this.loading = false;
				this.cdr.markForCheck();
			})
		).subscribe();
	}

	/**
	 * Checking control validation
	 *
	 * @param controlName: string => Equals to formControlName
	 * @param validationType: string => Equals to valitors name
	 */
	isControlHasError(controlName: string, validationType: string): boolean {
		const control = this.registerForm.controls[controlName];
		if (!control) {
			return false;
		}

		const result = control.hasError(validationType) && (control.dirty || control.touched);
		return result;
	}
}
