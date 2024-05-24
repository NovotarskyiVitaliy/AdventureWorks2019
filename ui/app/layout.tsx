import { Flex, Layout } from "antd";
import { Menu } from "antd";
import { Content, Footer, Header } from "antd/es/layout/layout";
import Link from "next/link";
import {Poppins} from 'next/font/google'
import  "./globals.css"

const poppins = Poppins (
  {
   weight: ["400", "700"] ,
   subsets: ["latin"]
  }
)

const items = [
  { key: "Home", label: <Link href={"/"}>Home</Link> },
  { key: "Table Ag-Grid", label: <Link href={"/table_agrid/1"}>Table AG Grid</Link> },
  { key: "Table Antd", label: <Link href={"/table_antd"}>Table Antd</Link> },
];

export default function RootLayout({ children})  {
  return (
    <html lang="en">
      <body suppressHydrationWarning={true} className={poppins.className}>
        <Layout style={{ minHeight: "100vh" }}>
          <Header>
            <Menu
              theme="dark"
              mode="horizontal"
              items={items}
              style={{ flex: 1, minWidth: 0 }}
            />
          </Header>
          <Content style={{ margin: "20px", padding: "60px" }}>{children}</Content>
          <Footer style={{ textAlign: "center",  fontSize: "20px"}}>
            Rest API using Next.js ui Training Project
          </Footer>
        </Layout>
      </body>
    </html>
  );
}
