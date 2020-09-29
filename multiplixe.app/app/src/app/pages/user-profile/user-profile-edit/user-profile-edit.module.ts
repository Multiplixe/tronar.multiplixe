import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { UserProfileEditPageRoutingModule } from './user-profile-edit-routing.module';

import { UserProfileEditPage } from './user-profile-edit.page';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    UserProfileEditPageRoutingModule ,
    ComponentModule
  ],
  declarations: [UserProfileEditPage]
})
export class UserProfileEditPageModule {}
