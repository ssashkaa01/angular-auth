import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';
import { AdminAreaComponent } from './Admin-area/Admin-area.component';
import { ClientAreaComponent } from './Client-area/Client-area.component';
import { HomeComponent } from './home/home.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';

const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'sign-in', pathMatch: 'full', component: SignInComponent },
    { path: 'sign-up', pathMatch: 'full', component: SignUpComponent },
    { path: 'admin-panel', pathMatch: 'full', component: AdminAreaComponent },
    { path: 'client-panel', pathMatch: 'full', component: ClientAreaComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
