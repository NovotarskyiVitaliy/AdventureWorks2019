import { useState, useEffect, useMemo } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import CustomUserCell from './customerUserCell';
import { UpdateEmployee, Employee } from "./updateEmployee";
import useCookie from 'react-use-cookie';
import { redirect } from 'next/navigation'

interface prop {
  url: string;
}

export default function UserGrid(props: prop) {

  const [userToken] = useCookie('myToken', '0');

  const requestOptions = {
    method: 'GET',
    headers: { 'Content-Type': 'application/json; charset=UTF-8;', 'Authorization': `${userToken}` },
  };


  const setValues = (e) => {
    setModalVisible(true);
    setEmployee(e.data);
  };

  const [columnDefs] = useState<any>([
    { headerName: 'businessEntityId', field: 'businessEntityId' },
    { headerName: 'firstName', field: 'firstName' },
    { headerName: 'lastName', field: 'lastName' },
    { field: 'name' },
    { field: 'groupName' },
    { field: 'departmentId' },
    {
      field: "actions",
      headerName: "Actions",
      cellRenderer: CustomUserCell,
      onCellClicked: (e: any) => setValues(e)
    }
  ]);

  const defaultColDef = useMemo(() => {
    return {
      filter: "agTextColumnFilter",
      floatingFilter: true,
    };

  }, []);

  useEffect(() => {
    if (userToken === "0") {
      redirect('/login');
    }

    fetch(props.url, requestOptions)
      .then((res) => res.json())
      .then((d) => setData(d))
      .catch(error => alert("Use login page to authorize"))
  }, []);

  const [persons, setData] = useState([]);
  const [visible, setModalVisible] = useState(false);
  const [employee, setEmployee] = useState({ firstName: "", lastName: "", businessEntityId: "", departmentId: "" });

  const onCreate = () => setModalVisible(false);

  return (
    <div className="ag-theme-alpine" style={{ height: '600px' }}>
      <UpdateEmployee
        visible={visible}
        setModalVisible={setModalVisible}
        employee={employee}
      />

      <AgGridReact
        rowData={persons}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        pagination={true}
        paginationPageSize={10}
        paginationPageSizeSelector={[5, 10, 25]}
      ></AgGridReact>
    </div>
  );
}