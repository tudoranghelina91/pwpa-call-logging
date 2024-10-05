import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ListLoggedCallsComponent } from './list-logged-calls.component';
import { ListLoggedCallsModule } from './list-logged-calls.module';
import { CallLoggerHttpClientService } from '../../services/call-logger-http-client.service';
import { of } from 'rxjs';
import { Router } from '@angular/router';

describe('ListLoggedCallsComponent', () => {
  let component: ListLoggedCallsComponent;
  let fixture: ComponentFixture<ListLoggedCallsComponent>;

  let callLoggerServiceSpy = jasmine.createSpyObj<CallLoggerHttpClientService>(['getLoggedCalls']);
  callLoggerServiceSpy.getLoggedCalls.and.returnValue(of([]));

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
});
