import { Directive, OnInit, ElementRef, Input } from "@angular/core";
import { AuthService } from "../../auth/auth.service";

@Directive({
  selector: '[netBoxAuthorization]',
  standalone: true
})
export class AuthorizationDirective implements OnInit {

  constructor(private el: ElementRef, private authorizationService: AuthService) { }

  @Input('netBoxAuthorization') rolePermission: Array<string>;

  ngOnInit(): void {
    if (!this.authorizationService.hasPermissionAuthorization(this.rolePermission)) {
      this.el.nativeElement.style.display = 'none';
      this.el.nativeElement.className = 'disappear';
      this.el.nativeElement.remove();
    }
  }

}
