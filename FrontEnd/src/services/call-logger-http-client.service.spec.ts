import { TestBed } from '@angular/core/testing';

import { CallLoggerHttpClientService } from './call-logger-http-client.service';

describe('CallLoggerHttpClientService', () => {
  let service: CallLoggerHttpClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CallLoggerHttpClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
