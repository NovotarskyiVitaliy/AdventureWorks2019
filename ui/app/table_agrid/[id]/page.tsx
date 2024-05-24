'use client'
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import UserGrid from "../../components/UserGrid"

export default function Table({ params: { id } }) {

  var url = "http://localhost:5124/Person?dId=" + id;

  return (
    <div className="ag-theme-alpine" style={{ height: '600px' }}>
      <UserGrid
        url={url}
      ></UserGrid>
    </div>
  );
}