import { BasePage } from './base.page';
import { EventEmitterService } from '../services/event-emitter.service';
import { AppInjector } from '../app-injector';
import { Subscription } from 'rxjs';
import { OnInit, OnDestroy } from '@angular/core';
import { LocalStorageService } from '../services/local-storage.service';
import { SocialMediaEnum } from '../enums/social-media.enum';

export class RestrictPage extends BasePage

    implements OnInit, OnDestroy {

    protected eventEmitter: EventEmitterService;
    private subscriptions: Subscription = new Subscription();
    public nickname: string = '';
    protected localStorageService: LocalStorageService;

    public socialMediaTwitter = SocialMediaEnum.twitter;
    public socialMediaFacebook = SocialMediaEnum.facebook;
    public socialMediaTwitch = SocialMediaEnum.twitch;
    public socialMediaYoutube = SocialMediaEnum.youtube;
    public rankingChanged: boolean = false;

    constructor() {
        super();
        this.eventEmitter = AppInjector.get(EventEmitterService);
        this.localStorageService = AppInjector.get(LocalStorageService);
        this.subscriptions = new Subscription();

        this.addSubscription(this.eventEmitter
            .get('ranking-updated')
            .subscribe(() => {
                this.zone.run(async () => {
                    this.rankingChanged = true;
                });
            }));
    }

    async ngOnInit() {
        super.ngOnInit();
        this.nickname = this.localStorageService.get("nickname");
    }

    ngOnDestroy(): void {
        this.subscriptions.unsubscribe();
    }

    addSubscription(s: Subscription) {
        this.subscriptions.add(s);
    }


}
