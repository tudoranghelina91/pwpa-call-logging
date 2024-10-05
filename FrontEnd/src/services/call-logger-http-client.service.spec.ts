import { TestBed } from '@angular/core/testing';

import { CallLoggerHttpClientService } from './call-logger-http-client.service';
import { HttpClient } from '@angular/common/http';

describe('CallLoggerHttpClientService', () => {
  let service: CallLoggerHttpClientService;

  beforeEach(() => {
    let httpSpy = jasmine.createSpyObj<HttpClient>('HttpClient', ['get', 'post']);

    TestBed.configureTestingModule({
      providers: [{ provide: HttpClient, useValue: httpSpy }]
    });
    service = TestBed.inject(CallLoggerHttpClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
