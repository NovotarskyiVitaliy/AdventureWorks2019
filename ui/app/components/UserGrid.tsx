import { useState, useEffect, useMemo } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import CustomUserCell from './customerUserCell';
import { CreateForm, Employee } from "@/app/components/CreateForm";

interface prop {
  url: string;
}

export default function UserGrid(props: prop) {

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
    console.log(props.url);

    try {
      fetch(props.url)
        .then((res) => res.json())
        .then((d) => setData(d))
    } catch (error) {
      console.log(error);
    }
  }, []);

  const [persons, setData] = useState([]);
  const [visible, setModalVisible] = useState(false);
  const [employee, setEmployee] = useState({ firstName: "", lastName: "", businessEntityId: "", departmentId: "" });

  const onCreate = () => setModalVisible(false);

  return (
    <div className="ag-theme-alpine" style={{ height: '600px' }}>
      <CreateForm
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