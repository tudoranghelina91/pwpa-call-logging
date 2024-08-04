import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Call } from '../models/Call';
import { ListLoggedCallsModule } from '../features/list-logged-calls/list-logged-calls.module';

@Injectable({
  providedIn: ListLoggedCallsModule,
})
export class CallLoggerHttpClientService {

  constructor(private client : HttpClient) { }

  getLoggedCalls() {
    return this.client.get<Call[]>('https://pwpa-call-logging-api:8003/calls');
  }

  logCall(call : Call) {
    return this.client.post<Call>('https://pwpa-call-logging-api:8003/calls', call);
  }
}
