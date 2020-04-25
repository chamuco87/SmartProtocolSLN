import { AuthNoticeService, AuthService, Register, User } from '../../../../core/auth/';
import { AbstractControl, AsyncValidatorFn } from "@angular/forms";
import { Observable, of } from "rxjs";
import { finalize, takeUntil, tap } from 'rxjs/operators';

function isEmptyInputValue(value: any): boolean {
	// we don't check for string here so it also works with arrays
	return value === null || value.length === 0;
}

export class EmailExistsValidator {
/**
 * Check if email already exists
 * @param control AbstractControl
 */

	constructor(
		private auth: AuthService
	) { }
	//static EmailExists(signupService: AuthService) {
	//	return (control: AbstractControl) => {
	//		return signupService.emailExists(control.value).pipe(tap(res => {
	//			return res ? null : { emailTaken: true };
	//		}));
	//	};
	//}

	static EmailExists(registerService: AuthService): AsyncValidatorFn {
		return (control: AbstractControl):
			| Promise<{ [key: string]: any } | null>
			| Observable<{ [key: string]: any } | null> => {
			if (isEmptyInputValue(control.value)) {
				return of(null);
			} else if (control.value === "") {
				return of(null);

			}
			else {
				return registerService.emailExists(control.value).toPromise().then(res => {
					return res ? null : { emailTaken: true };
				});
				//return control.valueChanges.pipe(
				//	tap(_ =>
				//		registerService
				//			.emailExists(control.value)
				//			.pipe(
				//				tap(user =>
				//					user ? { existingEmail: { value: control.value } } : null
				//				)
				//			)
				//	)
				//);
			}
		};
	}
}


//export class EmailExistsValidator {
//	constructor(private auth: AuthService) {
//	}
	
//	/**
//	 * Check if email already exists
//	 * @param control AbstractControl
//	 */
//	//EmailExists(control: AbstractControl) {

	


	
//}
