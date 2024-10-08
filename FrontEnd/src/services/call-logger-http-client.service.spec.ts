import { TestBed } from '@angular/core/testing';

import { CallLoggerHttpClientService } from './call-logger-http-client.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Call } from '../models/Call';

describe('CallLoggerHttpClientService', () => {
  let service: CallLoggerHttpClientService;
  let httpSpy = jasmine.createSpyObj<HttpClient>('HttpClient', ['get', 'post']);
  let data : Call = {
    callerName: 'test',
    address: 'test',
    description: 'test'
  };

  let base = environment.PWPA_CALL_LOGGING_API_BASE_URL;

  beforeEach(() => {

    httpSpy.get.withArgs(`${base}/calls`).and.stub();
    httpSpy.post.withArgs(`${base}/calls`, data).and.stub();

    TestBed.configureTestingModule({
      providers: [{ provide: HttpClient, useValue: httpSpy }]
    });
    service = TestBed.inject(CallLoggerHttpClientService);
  });

  afterEach(() => {
    httpSpy.get.calls.reset();
    httpSpy.post.calls.reset();
  })

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get calls from http service when getCalls called', () => {
    service.getLoggedCalls();
    expect(httpSpy.get).toHaveBeenCalledOnceWith(`${base}/calls`);
  });

  it('should insert call from http service when logCall called', () => {
    service.logCall(data);
    expect(httpSpy.post).toHaveBeenCalledOnceWith(`${base}/calls`, data);
  });
});
