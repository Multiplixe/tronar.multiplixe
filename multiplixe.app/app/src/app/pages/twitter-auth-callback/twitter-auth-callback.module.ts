import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TwitterAuthCallbackPageRoutingModule } from './twitter-auth-callback-routing.module';

import { TwitterAuthCallbackPage } from './twitter-auth-callback.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TwitterAuthCallbackPageRoutingModule
  ],
  declarations: [TwitterAuthCallbackPage]
})
export class TwitterAuthCallbackPageModule {}
