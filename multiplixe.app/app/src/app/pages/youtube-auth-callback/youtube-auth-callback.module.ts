import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { YoutubeAuthCallbackPageRoutingModule } from './youtube-auth-callback-routing.module';

import { YoutubeAuthCallbackPage } from './youtube-auth-callback.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    YoutubeAuthCallbackPageRoutingModule
  ],
  declarations: [YoutubeAuthCallbackPage]
})
export class YoutubeAuthCallbackPageModule {}
