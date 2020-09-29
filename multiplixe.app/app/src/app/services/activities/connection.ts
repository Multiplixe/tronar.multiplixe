import { IActivity } from './iactivity';
import { Activity as ActivityDto } from 'src/app/dtos/activity';
import { ActivitiesEnum } from 'src/app/enums/activities.enum';
import { BaseActivity } from './base-activity';

export class Connection extends BaseActivity implements IActivity {

   constructor() {
      super();
   }

   processActivity(dto: ActivityDto) {
      super.processActivity(dto, ActivitiesEnum.connection);
   }

   static create() {
      return new Connection();
   }

}