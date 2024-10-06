import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogCallComponent } from './log-call.component';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { LogCallModule } from './log-call.module';
import { of } from 'rxjs';
import { Call, CallFormModel } from '../../models/Call';

describe('LogCallComponent', () => {
  let component: LogCallComponent;
  let fixture: ComponentFixture<LogCallComponent>;

  let callLoggerServiceSpy = jasmine.createSpyObj<CallLoggerHttpClientService>(['logCall'])
  let callFormModel = new CallFormModel('','','');

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

  it('should not log call when form invalid', () => {
    component.form.controls['callerNameControl'].setValue('');
    component.submitCall();
    expect(callLoggerServiceSpy.logCall).toHaveBeenCalledTimes(0);
  });
});
