import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RedirectionComponent } from './components/redirection/redirection.component';
import { UrlTableComponent } from './components/url-table/url-table.component';
import { LoginComponent } from './components/login/login.component';
import { AboutComponent } from './components/about/about.component';

const routes: Routes = [
  { path: 'table', component: UrlTableComponent},
  { path: 'login', component: LoginComponent },
  { path: 'about', component: AboutComponent },
  { path: ':shortCode', component: RedirectionComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
