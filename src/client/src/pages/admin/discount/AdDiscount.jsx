import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import { faAdd, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Table } from "react-bootstrap";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import { useSnackbar } from "notistack";
import { useSelector } from "react-redux";
import { getFilterDiscountPagination, getVoucherDiscountPagination } from "../../../api/Discount";
import { format } from "date-fns";


const AdDiscount = () => {

  const [getDiscount, setgetDiscount] = useState([]);
  const [reRender, setRender] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true);

  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Voucher Discount";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetDiscount(props.items);
        setMetadata(props.metadata);
      }
      getVoucherDiscountPagination( ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
          console.log("Check data of voucher Discount", data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [ ps, p, reRender, pageNumber]);

  // const handleDeleteDiscount = (e, id) => {
  //   e.preventDefault();
  //   removeDiscount(id);
  //   async function removeDiscount(id) {
  //     if (window.confirm("Bạn có muốn Discount này")) {
  //       const response = await deleteDiscount(id);
  //       if (response) {
  //         enqueueSnackbar("Đã xoá thành công", {
  //           variant: "success",
  //         });
  //         setRender(true);
  //       } else {
  //         enqueueSnackbar("Đã xoá thành công", {
  //           variant: "error",
  //         });
  //       }
  //     }
  //   }
  // };
  



  return(
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Voucher Discount</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/discount/edit"} name="Thêm mới" />
        {/* <DiscountFilter /> */}
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Mã Discount</th>
                <th>Ngày bắt đầu</th>
                <th>Ngày kết thúc</th>
                <th>Số tiền giảm</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getDiscount.length > 0 ? (
                getDiscount.map((item, index) => (
                  <tr key={index}>
                    <td>{item.status}</td>
                    <td>{format(new Date(item.startDate), "dd/MM/yyyy")}</td>
                    <td>{format(new Date(item.endDate), "dd/MM/yyyy")}</td>
                    <td>{item.discountPrice}</td>
                    <td className="text-center">
                      <Link
                        to={`/admin/voucher/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      {/* <div onClick={(e) => handleDeleteDiscount(e, item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div> */}
                     
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={3}>
                    <h4 className="text-danger text-center">Không tìm thấy</h4>
                  </td>
                </tr>
              )}
            </tbody>
          </Table>
        )}
        <BtnNextPage metadata={metadata} onPageChange={updatePageNumber} />
      </div>
    </LayoutCommon>
  )
}
export default AdDiscount;