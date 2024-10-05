import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogCallComponent } from './log-call.component';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { LogCallModule } from './log-call.module';

describe('LogCallComponent', () => {
  let component: LogCallComponent;
  let fixture: ComponentFixture<LogCallComponent>;

  let callLoggerServiceSpy = jasmine.createSpyObj<CallLoggerHttpClientService>(['getLoggedCalls'])

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogCallModule],
      providers: [{provide: CallLoggerHttpClientService, useValue: callLoggerServiceSpy}]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LogCallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
