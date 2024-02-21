import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { ResponseResult } from "../../core/models/response-result.model";
import { BaseService } from "../../core/services/base.service";
import { LoginModel } from "./models/login.model";
import { appConstant } from "../../core/extenstion/app-constants";
import { isNull } from "../../core/extenstion/helper";
import { UserModel } from "./models/user.model";

@Injectable({
    providedIn: 'root',
})

export class AuthService extends BaseService {

    authenticationData: UserModel;

    constructor(private router: Router) {
        super();
    }

    login(loginModel: LoginModel): Observable<ResponseResult<string>> {
        return this.post<ResponseResult<string>>('security/authenticate', loginModel);
    }
  
    hasPermissionAuthorization(rolePermission: Array<string>) {
        const permissionClaims = JSON.parse(localStorage.getItem('claims')!);

        if (permissionClaims == null) {
            this.router.navigate(['login']);
            return false;
        }

        if (rolePermission != null || rolePermission != undefined) {
            if (rolePermission && rolePermission.find(role => {
                const isAvailable = permissionClaims.includes(role);
                return isAvailable;
            })) {
                return true;
            }
        }
        return false;
    }

    public get token(): string | null {
        let data = localStorage.getItem(appConstant.jwtTokenName);
        let token = '';

        if (!isNull(data))
            token = JSON.parse(data!).bearerToken;
        return token;
    }

    public get userId(): string | null {
        let data = localStorage.getItem(appConstant.jwtTokenName);
        let userId = '';

        if (!isNull(data))
            userId = JSON.parse(data!).userId;
        return userId;
    }

    public get authData(): UserModel | null {
        let data = localStorage.getItem(appConstant.jwtTokenName);

        if (!isNull(data))
            this.authenticationData = JSON.parse(data!);
        return this.authenticationData;
    }

    public get isAuthenticated(): boolean {
        return this.token !== null;
    }
}
