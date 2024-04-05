import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RedirectionComponent } from './components/redirection/redirection.component';

const routes: Routes = [
  { path: ':shortCode', component: RedirectionComponent },
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
