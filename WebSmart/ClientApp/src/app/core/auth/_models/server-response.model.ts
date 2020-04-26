export class ServerResponse{
	isSuccess: boolean;
	data: any;
	errors: Array<ErrorViewModel>;
}

export class ErrorViewModel {
	errorCode: string;
	errorDetail: string;
}
