import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignUserDialogComponent } from './assign-user-dialog.component';

describe('AssignUserDialogComponent', () => {
  let component: AssignUserDialogComponent;
  let fixture: ComponentFixture<AssignUserDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AssignUserDialogComponent]
    });
    fixture = TestBed.createComponent(AssignUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
