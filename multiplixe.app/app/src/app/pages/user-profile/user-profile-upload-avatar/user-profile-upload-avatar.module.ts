import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { UserProfileUploadAvatarRoutingModule } from './user-profile-upload-avatar-routing.module';
import { UserProfileUploadAvatarPage } from './user-profile-upload-avatar.page';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    UserProfileUploadAvatarRoutingModule,
    SharedModule,
    ComponentModule
  ],
  declarations: [UserProfileUploadAvatarPage]
})
export class UserProfileUploadAvatarModule {}
