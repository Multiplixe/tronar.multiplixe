import { Activity as ActivityDto } from 'src/app/dtos/activity';
import { EventEmitterService } from '../event-emitter.service';
import { AppInjector } from 'src/app/app-injector';
import { ActivitiesEnum } from 'src/app/enums/activities.enum';

export class BaseActivity {

   public eventEmitter: EventEmitterService;

   constructor() {
      this.eventEmitter = AppInjector.get(EventEmitterService)
   }

   processActivity(dto: ActivityDto, e: ActivitiesEnum) {
      let emitter = this.eventEmitter.get(e);
      emitter.emit(dto);
   }

}