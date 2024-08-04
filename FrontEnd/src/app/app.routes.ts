import { Routes } from '@angular/router';
import { ListLoggedCallsComponent } from '../features/list-logged-calls/list-logged-calls.component';
import { LogCallComponent } from '../features/log-call/log-call.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'calls',
        pathMatch: 'full'
    },
    {
        path: 'calls',
        component: ListLoggedCallsComponent
    },
    {
        path: 'calls/log',
        component: LogCallComponent
    }
];
