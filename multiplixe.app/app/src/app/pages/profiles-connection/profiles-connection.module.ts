import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';

import { ProfilesConnectionPageRoutingModule } from './profiles-connection-routing.module';

import { ProfilesConnectionPage } from './profiles-connection.page';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ProfilesConnectionPageRoutingModule,
    ComponentModule,
    SharedModule
  ],
  declarations: [ProfilesConnectionPage]
})
export class ProfilesConnectionPageModule { }
