import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrendsComponent } from './trends/trends.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: 'user', component: UserComponent },
  { path: 'trends', component: TrendsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
