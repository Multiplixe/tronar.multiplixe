import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserProfileUploadAvatarPage } from './user-profile-upload-avatar.page';

const routes: Routes = [
  {
    path: '',
    component: UserProfileUploadAvatarPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserProfileUploadAvatarRoutingModule {}
