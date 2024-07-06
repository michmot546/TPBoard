import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectTablesComponent } from './project-tables.component';

describe('ProjectTablesComponent', () => {
  let component: ProjectTablesComponent;
  let fixture: ComponentFixture<ProjectTablesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectTablesComponent]
    });
    fixture = TestBed.createComponent(ProjectTablesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
