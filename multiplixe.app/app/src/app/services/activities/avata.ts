import { IActivity } from './iactivity';
import { Activity as ActivityDto } from 'src/app/dtos/activity';
import { ActivitiesEnum } from 'src/app/enums/activities.enum';
import { BaseActivity } from './base-activity';

export class Avatar extends BaseActivity implements IActivity {

   constructor() {
      super();
   }

   processActivity(dto: ActivityDto) {
      super.processActivity(dto, ActivitiesEnum.avatar);
   }

   static create() {
      return new Avatar();
   }

}