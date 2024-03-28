import React, { useEffect, useState } from "react";
import { useSnackbar } from "notistack";
import { useSelector } from "react-redux";
import { Link, useParams } from "react-router-dom";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import CommentFilter from "../../../components/admin/filter/CommentFilter";
import { deleteComment, getFilterComment } from "../../../api/Comment";
import { faAdd, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import { format } from "date-fns";

const AdComments = () => {
  const [getCommnent, setgetCommnent] = useState([]);
  const [reRender, setRender] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    commentFilter = useSelector((state) => state.commentFilter);

  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Commnet";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetCommnent(props.items);
        setMetadata(props.metadata);
      }
      getFilterComment(commentFilter.name, ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
          console.log("Checkdata", data)
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [commentFilter, ps, p, reRender, pageNumber]);

  const handleDeletComment = (e, id) => {
    e.preventDefault();
    removeComment(id);
    async function removeComment(id) {
      if (window.confirm("Bạn có muốn unit này")) {
        const response = await deleteComment(id);
        if (response) {
          enqueueSnackbar("Đã xoá thành công", {
            variant: "success",
          });
          setRender(true);
        } else {
          enqueueSnackbar("Lỗi trong khi xoá", {
            variant: "error",
          });
        }
      }
    }
  };
  

  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Comment</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/comment/edit"} name="Thêm mới" />
        <CommentFilter />
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Người Bình Luận</th>
                <th>Số đánh giá</th>
                <th>Ngày đánh giá</th>
                <th>Trạng thái</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getCommnent.length > 0 ? (
                getCommnent.map((item, index) => (
                  <tr key={index}>
                    <td>{item.name}</td>
                    <td>{item.rating}</td>
                    <td>{format(new Date(item.created), "dd/MM/yyyy")}</td>
                    <td>{item.status}</td>

                    <td className="text-center">
                      <Link
                        to={`/admin/comment/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={(e) => handleDeletComment(e, item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
                     
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={6}>
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
  );
};
export default AdComments;
