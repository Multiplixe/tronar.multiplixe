import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { YoutubePageRoutingModule } from './youtube-routing.module';

import { YoutubePage } from './youtube.page';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    SharedModule,
    ComponentModule,
    YoutubePageRoutingModule
  ],
  declarations: [YoutubePage]
})
export class YoutubePageModule { }
