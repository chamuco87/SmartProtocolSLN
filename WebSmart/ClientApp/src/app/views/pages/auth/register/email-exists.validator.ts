import { AuthNoticeService, AuthService, Register, User } from '../../../../core/auth/';
import { AbstractControl, AsyncValidatorFn, ValidationErrors  } from "@angular/forms";
import { Observable, of } from "rxjs";
import { finalize, takeUntil, tap, map, debounceTime, take, switchMap } from 'rxjs/operators';
import { ApiRegister } from '../../../../core/auth/_models/api-register.model';

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
	) {
		
	}
	//static EmailExists(signupService: AuthService) {
	//	return (control: AbstractControl) => {
	//		return signupService.emailExists(control.value).pipe(tap(res => {
	//			return res ? null : { emailTaken: true };
	//		}));
	//	};
	//}

	public apiUser: ApiRegister = new ApiRegister();
	static EmailExists(registerService: AuthService): AsyncValidatorFn {
		return (control: AbstractControl):
			| Promise<ValidationErrors  | null>
			| Observable<ValidationErrors | null> => {
			if (isEmptyInputValue(control.value)) {
				return of(null);
			} else if (control.value === "") {
				return of(null);

			}
			else {
				//return registerService.emailExists(control.value).pipe(map(res => {
				//	return res ? null : { emailTaken: true };
				//}))
				return control.valueChanges.pipe(
					debounceTime(500),
					take(1),
					switchMap(_ =>
						registerService.emailExists(control.value)
							.pipe(
								map(apiUser => {
									if (apiUser.Email == 'true') {
										return { "emailExists": true }
									}
									else {
										return null;
									}
									//return apiUser?{ "emailExists": true } : null;
								}
								)
							)
					)
				);
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
