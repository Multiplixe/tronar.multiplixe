import { NgModule } from '@angular/core';
import { SharedModule } from '../shared-module/shared.module';
import { SocialMediaProfileFacebookComponent } from './social-media-profile-connections/facebook/social-media-profile-facebook.component'
import { SocialMediaProfileTwitterComponent } from './social-media-profile-connections/twitter/social-media-profile-twitter.component'
import { SocialMediaProfileTwitchComponent } from './social-media-profile-connections/twitch/social-media-profile-twitch.component'
import { HomeScreenComponent } from './home-screen/home-screen.component';
import { PwaUpdateComponent } from './pwa-update/pwa-update.component';
import { UserProfileAvatarComponent } from './user-profile-avatar/user-profile-avatar.component';
import { LoaderComponent } from './loader/loader.component';
import { HeaderComponent } from './header/header.component';
import { UserProfileFormComponent } from './user-profile-form/user-profile-form.component';
import { SocialMediaProfileDashboardComponent } from './social-media-profile-dashboard/social-media-profile-dashboard.component';
import { ListItemsComponent } from './list-items/list-items.component';
import { SocialMediaConnectionComponent } from './social-media-connection/social-media-connection.component';
import { SocialMediaConnectionDisplayComponent } from './social-media-connection-display/social-media-connection-display.component';
import { SocialMediaConnectionActionComponent } from './social-media-connection-action/social-media-connection-action.component';
import { LegalLinksComponent } from './legal-links/legal-links.component';

@NgModule({
    imports: [SharedModule],
    declarations: [SocialMediaProfileFacebookComponent, SocialMediaProfileTwitterComponent, SocialMediaProfileTwitchComponent, HomeScreenComponent, PwaUpdateComponent, UserProfileAvatarComponent, LoaderComponent, HeaderComponent, UserProfileFormComponent, SocialMediaProfileDashboardComponent, ListItemsComponent, SocialMediaConnectionComponent, SocialMediaConnectionDisplayComponent, SocialMediaConnectionActionComponent, LegalLinksComponent],
    exports: [SocialMediaProfileFacebookComponent, SocialMediaProfileTwitterComponent, SocialMediaProfileTwitchComponent, HomeScreenComponent, PwaUpdateComponent, UserProfileAvatarComponent, LoaderComponent, HeaderComponent, UserProfileFormComponent, SocialMediaProfileDashboardComponent, ListItemsComponent, SocialMediaConnectionComponent, SocialMediaConnectionDisplayComponent, SocialMediaConnectionActionComponent, LegalLinksComponent]
})
export class ComponentModule { }