'use client'

import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import UserGrid from "../components/userGrid"
import {InitObvject} from  "../infrastructure/initObject"
export default function Table() {

  var url = `${InitObvject.domain}/api/Person/GetEmployees`

  return (
    <div className="ag-theme-alpine" style={{ height: '600px' }}>
      <UserGrid
        url={url}
      ></UserGrid>
    </div>
  );
}