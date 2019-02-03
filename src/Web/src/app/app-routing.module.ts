
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { BatchesComponent } from './batches';
import { StocksComponent } from './stocks';
import { AdminComponent } from './admin';
import { LoginComponent } from './login';
import { AuthGuard } from './_guards';
import { Role } from './_models';

const routes: Routes = [
  {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'batches',
        component: BatchesComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.User, Role.Admin] }
    },
    {
        path: 'stocks',
        component: StocksComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.User, Role.Admin] }
    },
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'login',
        component: LoginComponent
    },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
export const routing = RouterModule.forRoot(routes);
