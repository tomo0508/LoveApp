import { Photo } from './photo';

export interface User {

    id: number;
    username: string;
    knownAs: string;
    gender: string;
    created: DataCue;
    lastActive: Date;
    photoUrl: string;
    country: string;
    intrests?: string;
    interduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
