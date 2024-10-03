import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Call } from '../models/Call';
import { ListLoggedCallsModule } from '../features/list-logged-calls/list-logged-calls.module';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: ListLoggedCallsModule,
})
export class CallLoggerHttpClientService {

  constructor(private client : HttpClient) { }

  // TODO: USE BASE URL BASED ON DOCKER ENVIRONMENT VARIABLES
  getLoggedCalls() {
    let base = environment.PWPA_CALL_LOGGING_API_BASE_URL;
    return this.client.get<Call[]>(`${base}/calls`);
  }

  logCall(call : Call) {
    let base = environment.PWPA_CALL_LOGGING_API_BASE_URL;
    return this.client.post<Call>(`${base}/calls`, call);
  }
}
