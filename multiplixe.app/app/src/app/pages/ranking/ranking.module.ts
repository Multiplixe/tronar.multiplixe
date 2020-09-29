import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { RankingPageRoutingModule } from './ranking-routing.module';
import { RankingPage } from './ranking.page';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RankingPageRoutingModule,
    SharedModule,
    ComponentModule
  ],
  declarations: [RankingPage]
})
export class RankingPageModule {}
