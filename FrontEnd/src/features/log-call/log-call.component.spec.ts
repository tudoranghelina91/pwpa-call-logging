import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogCallComponent } from './log-call.component';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { LogCallModule } from './log-call.module';
import { of, throwError } from 'rxjs';
import { Router } from '@angular/router';

describe('LogCallComponent', () => {
  let component: LogCallComponent;
  let fixture: ComponentFixture<LogCallComponent>;

  let routerSpy = jasmine.createSpyObj<Router>(['navigate']);
  routerSpy.navigate.and.callThrough();
  
  let callLoggerServiceSpy = jasmine.createSpyObj<CallLoggerHttpClientService>(['logCall']);
  callLoggerServiceSpy.logCall.and.returnValue(of(
    {
      callerName: '',
      address: '',
      description: ''
    }));

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogCallModule],
      providers: [
        {
          provide: CallLoggerHttpClientService, 
          useValue: callLoggerServiceSpy
        },
        {
          provide: Router,
          useValue: routerSpy
        }
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LogCallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  afterEach(async() => {
    callLoggerServiceSpy.logCall.calls.reset();
    routerSpy.navigate.calls.reset();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not log call when form invalid', () => {
    component.form.controls['callerNameControl'].setValue('');
    fixture.detectChanges();
    component.submitCall();
    expect(callLoggerServiceSpy.logCall).toHaveBeenCalledTimes(0);
  });

  it('should submit when form valid', () => {
    component.form.controls['callerNameControl'].setValue('test');
    component.form.controls['addressControl'].setValue('test');
    component.form.controls['descriptionControl'].setValue('test');

    fixture.detectChanges();
    component.submitCall();

    expect(callLoggerServiceSpy.logCall).toHaveBeenCalledTimes(1);
    expect(routerSpy.navigate).toHaveBeenCalledOnceWith(['..']);
  });
});