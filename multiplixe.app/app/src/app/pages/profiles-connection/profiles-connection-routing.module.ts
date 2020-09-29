import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProfilesConnectionPage } from './profiles-connection.page';

const routes: Routes = [
  {
    path: '',
    component: ProfilesConnectionPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProfilesConnectionPageRoutingModule {}
