import { StringEntry } from '../entries/string.entry';
import { BaseEntries } from './base-entries';
import { UserProfile } from './user-profile';

export class UserProfileEntry extends BaseEntries {
    name: StringEntry = StringEntry.Create();
    nickname: StringEntry = StringEntry.Create();
    email: StringEntry = StringEntry.Create();
    password: StringEntry = StringEntry.Create();
    confirmPassword: string = '';

    static Create(): UserProfileEntry {
        return new UserProfileEntry();
    }

    static Merge(profile: UserProfile): UserProfileEntry {
        var entry = new UserProfileEntry();
        entry.nickname.value = profile.nickname;
        entry.name.value = profile.name;
        entry.email.value = profile.email;
        return entry;
    }

    static extract(entry: UserProfileEntry): UserProfile {
        let profile = new UserProfile();
        profile.nickname = entry.nickname.value;
        profile.name = entry.name.value;
        profile.email = entry.email.value;

        return profile;
    }

}

export default UserProfileEntry;