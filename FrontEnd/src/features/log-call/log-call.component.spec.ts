import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogCallComponent } from './log-call.component';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { LogCallModule } from './log-call.module';
import { of } from 'rxjs';
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

  it('should recheck form validity on clearErrors', () => {
    component.clearErrors('callerNameControl');
    component.clearErrors('addressControl');
    component.clearErrors('descriptionControl');

    expect(component.isCallerNameValid).toBe(component.form.controls['callerNameControl'].valid);
    expect(component.isAddressValid).toBe(component.form.controls['addressControl'].valid);
    expect(component.isDescriptionValid).toBe(component.form.controls['descriptionControl'].valid);
  });

  it('should isCallerNameValid be true when callerNameControl is valid', () => {
    component.form.controls['callerNameControl'].setValue('test');
    component.clearErrors('callerNameControl');
    expect(component.isCallerNameValid).toBeTrue();
  });

  it('should isCallerNameValid be false when callerNameControl is not valid', () => {
    component.form.controls['callerNameControl'].setValue('');
    component.clearErrors('callerNameControl');
    expect(component.isCallerNameValid).toBeFalse();
  });

  it('should isAddressValid be true when addressControl is valid', () => {
    component.form.controls['addressControl'].setValue('test');
    component.clearErrors('addressControl');
    expect(component.isAddressValid).toBeTrue();
  });

  it('should isAddressValid be false when addressControl is not valid', () => {
    component.form.controls['addressControl'].setValue('');
    component.clearErrors('addressControl');
    expect(component.isAddressValid).toBeFalse();
  });

  it('should isDescriptionValid be true when descriptionControl is valid', () => {
    component.form.controls['descriptionControl'].setValue('test');
    component.clearErrors('descriptionControl');
    expect(component.isDescriptionValid).toBeTrue();
  });

  it('should isDescriptionValid be false when descriptionControl is not valid', () => {
    component.form.controls['descriptionControl'].setValue('');
    component.clearErrors('descriptionControl');
    expect(component.isDescriptionValid).toBeFalse();
  });
});