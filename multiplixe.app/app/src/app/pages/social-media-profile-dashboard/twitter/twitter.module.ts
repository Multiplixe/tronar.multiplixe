import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TwitterPageRoutingModule } from './twitter-routing.module';

import { TwitterPage } from './twitter.page';
import { ComponentModule } from 'src/app/components/component.module';
import { SharedModule } from 'src/app/shared-module/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TwitterPageRoutingModule,
    SharedModule,
    ComponentModule
  ],
  declarations: [TwitterPage]
})
export class TwitterPageModule {}
