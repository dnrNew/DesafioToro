import { UserAsset } from "./user-assets";

export class User {
    id: number;
    name: string;
    cpf: string;
    account: string;
    balance: number;
    userAssets: UserAsset[]
}