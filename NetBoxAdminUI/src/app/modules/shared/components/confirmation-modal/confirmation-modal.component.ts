import { ChangeDetectionStrategy, Component, HostListener, Inject } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'netbox-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.scss',
  standalone: true,
  imports: [SharedModule]
})
export class ConfirmationModalComponent {
  @HostListener("keydown.esc")
  public onEsc() {
    this.close(false);
  }

  constructor(@Inject(MAT_DIALOG_DATA) public data: { message: string, title: string },
    private mdDialogRef: MatDialogRef<ConfirmationModalComponent>) { }

  ngOnInit(): void {
  }

  public cancel() {
    this.close(false);
  }
  public close(value: any) {
    this.mdDialogRef.close(value);
  }
  public confirm() {
    this.close(true);
  }
}
