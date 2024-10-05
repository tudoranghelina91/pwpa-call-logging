import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Call } from '../models/Call';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CallLoggerHttpClientService {

  constructor(private client : HttpClient) { }

  getLoggedCalls() {
    let base = environment.PWPA_CALL_LOGGING_API_BASE_URL;
    return this.client.get<Call[]>(`${base}/calls`);
  }

  logCall(call : Call) {
    let base = environment.PWPA_CALL_LOGGING_API_BASE_URL;
    return this.client.post<Call>(`${base}/calls`, call);
  }
}
