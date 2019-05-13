import { Photo } from './photo';

export interface User {
    id: number;
    userName: string;
    knownAs: string;
    age: number;
    gender: string;
    // createdDate: Date;
    createdDate: string;
    lastActive: Date;
    photoUrl: string;
    city: string;
    province: string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
    photos?: Photo[];
}
