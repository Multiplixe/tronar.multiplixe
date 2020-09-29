import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { FacebookPageRoutingModule } from './facebook-routing.module';

import { FacebookPage } from './facebook.page';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    FacebookPageRoutingModule,
    SharedModule,
    ComponentModule      
  ],
  declarations: [FacebookPage]
})
export class FacebookPageModule {}
