import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Call } from '../models/Call';
import { ListLoggedCallsModule } from '../features/list-logged-calls/list-logged-calls.module';

@Injectable({
  providedIn: ListLoggedCallsModule,
})
export class CallLoggerHttpClientService {

  constructor(private client : HttpClient) { }

  // TODO: USE BASE URL BASED ON DOCKER ENVIRONMENT VARIABLES
  getLoggedCalls() {
    return this.client.get<Call[]>('https://192.168.1.133:8003/calls');
  }

  logCall(call : Call) {
    return this.client.post<Call>('https://192.168.1.133:8003/calls', call);
  }
}
