import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import { faAdd, faEdit, faInfoCircle, faTrash } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import { Table } from "react-bootstrap";
import { useSnackbar } from "notistack";
import { deleteOrder, getOderPagination } from "../../../api/Order";
import { format } from "date-fns";
import Popup from "../../../components/common/Popup";

const AdOrder = () => {
  const [getOrder, setgetOrder] = useState([]);
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
    document.title = "Quản lý Đơn hàng";

    loadOrder();
    async function loadOrder() {
      function setData(props) {
        setgetOrder(props.items);
        setMetadata(props.metadata);
      }
      getOderPagination(ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
          console.log("Check data order", data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [ps, p, reRender, pageNumber]);

  const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [IdToDelete, setIdToDelete] = useState(null);

  const handleDelete = (id) => {
    setIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá đơn hàng này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deleteOrder(IdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRender((prev) => !prev);
    } else {
      enqueueSnackbar("Xoá thất bại", {
        variant: "error",
      });
    }
    setPopupVisible(false);
  };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };


  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Đơn hàng</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/order/edit"} name="Thêm mới" />
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>ID đơn</th>
                <th>Ngày đặt đơn hàng</th>
                <th>Tổng tiền đơn hàng</th>
                <th>Thông tin</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getOrder.length > 0 ? (
                getOrder.map((item, index) => (
                  <tr key={index}>
                    <td>{item.id}</td>
                    <td>{format(new Date(item.dateOrder), "dd/MM/yyyy")}</td>
                    <td>
                    {formatCurrency(item.totalPrice)}</td>
                    <td className="text-center">
                      <Link
                        to={`/admin/order/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faInfoCircle} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDelete(item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
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
      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancelDelete}
          onConfirm={handleConfirmDelete}
        />
      )}

    </LayoutCommon>
  );
};
export default AdOrder;
