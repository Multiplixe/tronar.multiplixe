export class SocialMediaProfile {
    public profileId: string = '';
    public name: string = '';
    public imageUrl: string = '';
    public token: string = '';

    static create() {
        return new SocialMediaProfile();
    }
}