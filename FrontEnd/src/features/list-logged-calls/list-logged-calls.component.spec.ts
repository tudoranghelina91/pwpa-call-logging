import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ListLoggedCallsComponent } from './list-logged-calls.component';
import { ListLoggedCallsModule } from './list-logged-calls.module';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { of, throwError } from 'rxjs';
import { Router } from '@angular/router';

describe('ListLoggedCallsComponent', () => {
  let component: ListLoggedCallsComponent;
  let fixture: ComponentFixture<ListLoggedCallsComponent>;
  let testData = [{
    callerName: 'test',
    address: 'test',
    description: 'test',
  }];

  let callLoggerServiceSpy = jasmine.createSpyObj<CallLoggerHttpClientService>(['getLoggedCalls']);
  callLoggerServiceSpy.getLoggedCalls.and.returnValue(of(testData));

  let routerSpy = jasmine.createSpyObj<Router>(['navigateByUrl']);
  routerSpy.navigateByUrl.and.callThrough();

  beforeEach(async () => {

    await TestBed.configureTestingModule({
      imports: [ListLoggedCallsModule],
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

    fixture = TestBed.createComponent(ListLoggedCallsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call router navigateByUrl', () => {
    component.gotoLogCall();
    expect(routerSpy.navigateByUrl).toHaveBeenCalled();
  });

  it('should fetch logged calls when ngOnInit called', () => {
    component.ngOnInit();
    expect(callLoggerServiceSpy.getLoggedCalls).toHaveBeenCalled();
    expect(component.calls).toEqual(testData)
  });

  it('should set error when ngOnInitCalled and service throws error', () => {
    callLoggerServiceSpy.getLoggedCalls.and.returnValue(throwError(() => new Error()));
    component.ngOnInit();
    expect(component.canConnect).toBeFalse();
  });
});
